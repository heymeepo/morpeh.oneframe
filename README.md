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
You haven't to setup any manually, plugin will be initialized automatically after installation.

There are two options to use oneframes ```entity.OneFrame<T>()``` and ```world.OneFrame<T>()```

```csharp
public override void OnUpdate(float deltaTime)
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
Oneframe component will always be deleted at the end of the frame automatically.

You can use overloaded version of this methods as well:

```csharp
public override void OnAwake()
{
    World.OneFrame(new CreateUnitRequest() { config = configService.GetUnitConfig("Zombie") });
}
```

```csharp
public sealed class EnemyFactorySystem : ISystem
{
    private Filter filter;

    public override void OnAwake()
    {
        filter = World.Filter.With<CreateUnitRequest>();
    }

    public override void OnUpdate(float deltaTime)
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
Plugin use an entity pool to avoid frequent unnecessary memory allocations, and if you want to use oneframe entity, created by ```World.OneFrame<T>()``` in future, you have to release it with ```entity.ReleaseOneFrame()``` , otherwise you may encounter bugs and unpredictable behavior. Oneframe component will also be deleted at the ```end``` of the frame in this case.

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