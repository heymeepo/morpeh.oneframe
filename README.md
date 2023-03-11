# Morpeh OneFrames

[Morpeh ECS](https://github.com/scellecs/morpeh) oneframes extention.

## Installation

Unity version 2021.3.* LTS

C# 9.0 Required!

Install with Package Manager via git url

```bash
https://github.com/heymeepo/morpeh.oneframe.git
```

## Usage
You haven't to setup any manually, oneframe components will always be deleted at the end of the frame automatically.

There are two options to use oneframes ```entity.OneFrame<T>()``` and ```world.OneFrame<T>()```

```csharp
public void OnUpdate(float deltaTime)
{
    foreach (var entity in filter)
    {
        var health = entity.GetComponent<Health>();

        if(health.value <= 0)
        {
            entity.OneFrame<DeathMarker>();
        }
    }
}
```

You can use overloaded versions of this methods as well:

```csharp
public void OnAwake()
{
    World.OneFrame(new CreateUnitRequest() { config = configService.GetUnitConfig("Zombie") });
}
```

```csharp
public sealed class EnemyFactorySystem : ISystem
{
    private Filter filter;

    public void OnAwake()
    {
        filter = World.Filter.With<CreateUnitRequest>();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            var config = entity.GetComponent<CreateUnitRequest>().config;

            var unit = World.CreateEntity();
            unit.SetComponent(new HealthComponent() { maxValue = config.Health });
            ...
        }
    }
}
```
Plugin uses an entity pool to avoid excessive memory allocations, if you want to use oneframe entity, created by ```World.OneFrame<T>()``` in future, you have to release it with ```entity.ReleaseOneFrame()``` , otherwise you may encounter bugs and unpredictable behavior. Oneframe component will also be deleted at the ```end``` of the frame in this case.

```csharp
public sealed class EnemyFactorySystem : ISystem
{
    ...
    public override void OnUpdate(float deltaTime)
    {
        foreach (var entity in filter)
        {
            var config = entity.GetComponent<CreateUnitRequest>().config;

            entity.ReleaseOneFrame();
            entity.SetComponent(new HealthComponent() { maxValue = config.Health });
            ...
        }
    }
}
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
