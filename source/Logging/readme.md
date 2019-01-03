# DotNetCore.Logging

The package provides interface and classe for **logging**.

## ILogger

```cs
public interface ILogger
{
    void Error(Exception exception);
}
```

## Logger

The **Logger** class uses the **Serilog** package. It reads the settings from the **AppSettings.json** file.

```cs
public class Logger : ILogger
{
    public Logger(IConfiguration configuration) { }

    public void Error(Exception exception) { }
}
```
