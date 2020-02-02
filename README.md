# JsonApi.Client
A .NET Standard library for consuming the JsonApi https://jsonapi.gunterweb.ca

## Add JSON Object
	Client c = new Client("apkiKey");
 	string dataId = await c.CreateObject("usersBucket", "{userId:1}");
	
## Update JSON Object
	Client c = new Client("apkiKey");
  	bool success = await c.UpdateObject("usersBucket", "cff924d8a5324673b997cebfc2682081", "{userId:1,name:\"bob\"}");
	
## Delete JSON Object
	Client c = new Client("apkiKey");
  	bool success = await c.DeleteObject("usersBucket", "cff924d8a5324673b997cebfc2682081");
