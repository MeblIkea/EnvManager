# Environment Manager
Create an environment for a program, so everything is serialized and stored in a Json .csenv file.<br>
Useful for when sharing programs, and you all have differenet environemnts.

## Usage
Let's define a structure for the tests:
```cs
struct CarStruct {
    public string Type;
    public string Model;
    public string Brand;
}
```

Let's instantiate it, and save it in our environemnt, under the id *Car.Parking*, and under the attribute *cars*.
```cs
CarStruct car = new() {
    Type = "City Car",
    Brand = "Renault",
    Model = "Twingo"
};
Env parkingEnv = EnvironmentManager.GetEnv("Car.Parking", "Parking Manager"); // Name is just a description for the JSON
parkingEnv.SetAttribute("cars", new[] {car});
```
The .csenv looks like the following:
```json
{
  "Car.Parking": {
    "Name": "Parking Manager",
    "Attributes": {
      "cars": [
        {
          "Type": "City Car",
          "Model": "Twingo",
          "Brand": "Renault"
        }
      ]
    }
  }
}
```

You can now simply get your values like:
```json
Env env = EnvironmentManager.GetEnv("Car.Parking"); // It doesn't matter whether we include the name or not
Console.WriteLine(env.GetAttribute<CarStruct[]>("cars")[0].Model);
```
<br>*So technically, all you would ever need for prods is to get the envs, as it's not designed to save values dynamically.* 
