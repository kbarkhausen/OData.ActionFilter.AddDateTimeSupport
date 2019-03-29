# OData.ActionFilter.AddDateTimeSupport
This action filter was created to fix a limitation in:

* Microsoft.AspNet.OData v7.x

You can find out more about this issue on:

[OData issue:  OData V4 service should support DateTime](https://github.com/OData/WebApi/issues/136)

### How it works

This filter performs the following steps on each request:

- Extracts the $filter=[query] section from any OData request Uri
- Using Regex, looks for any values in this section that match the following Date value formats : 'MM/DD/YYYY', 'YYYY-MM-DD' or YYYY-MM-DD (no quotes)
- Converts this value into a DateTimeOffset string value (i.e. "yyyy-MM-ddT00:00:00KZ")
- Replaces the Date value in the OData request Uri with the new DateTimeOffset value
- The updated Uri is then processed without any errors by the OData libraries

For example, the following OData request:

`http://localhost/odata/People?$filter=Birtdate gt '1978-01-01'`

Would become:

`http://localhost/odata/People?$filter=Birtdate gt '1978-01-01T00:00:00KZ'`


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

If you run into any problems or issues, you can report your issues on GitHub. This source is provided for free and with no support. I will try to fix any issues reported when I have some spare time.

* [Bug Reports and Feature Requests](https://github.com/kbarkhausen/OData.ActionFilter.AddDateTimeSupport/issues)

