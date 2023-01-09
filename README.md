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

You need to create ```ECS/Systems/OneFrameSystem``` and add it to the end of your systems hierarchy in Installer as LateUpdateSystem.

Now you can use oneframes in your systems as follows:


```csharp
public override void OnAwake()
{
    World.OneFrame<PlayerInputJumpRequest>();
    World.OneFrame(new CreateUnitRequest() { config = configService.GetUnitConfig("Zombie"), pos = Vector3.one };)
}

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
Oneframes will always be deleted at the end of the frame automatically.


If you want to use oneframe entity, created by ```World.OneFrame<T>()``` in future, you have to release it with ```entity.ReleaseOneFrame()```

```csharp
private Filter filter;

public override void OnAwake()
{
    filter = World.Filter.With<CreateUnitRequest>();
}

public override void OnUpdate(float deltaTime)
{
    foreach (var entity in filter)
    {
        entity.ReleaseOneFrame();
        entity.AddComponent<Health>();
    }
}
```

Oneframe component will also be deleted at the end of the frame, but not the entity in this case.

MultiWords are not supported. Works only with ```World.Default```

## License

[MIT](https://choosealicense.com/licenses/mit/)
