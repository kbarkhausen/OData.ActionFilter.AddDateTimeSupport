# OData.ActionFilter.AddDateTimeSupport
This action filter was created to fix a limitation in:

* Microsoft.AspNet.OData v7.x

You can find out more about this issue on:


### Implementing this filter

You can use this action filter by applying it to a custom base Controller class:

```csharp
[ODataDateTimeSupportActionFilter]
public abstract class MyODataControllerBase : Microsoft.AspNet.OData.ODataController
{
        
}


public class MyODataController : MyODataControllerBase
{
   // implement your OData methods here
}
```

You can use this action filter by applying it to specific methods as well:

```csharp
public class MyODataController : Microsoft.AspNet.OData.ODataController
{
   [ODataDateTimeSupportActionFilter]
   [EnableQuery]
   public IQueryable<Student> Get()
   {
       // method logic here
   }
}
```
### Problems, Comments and/or Suggestions? Please let me know

If you run into any problems or issues, you can report issues on GitHub. This is provided for free and with no support. I will try to fix any issues reported when I have some spare time.

* [Bug Reports and Feature Requests](https://github.com/kbarkhausen/OData.ActionFilter.AddDateTimeSupport/issues)
