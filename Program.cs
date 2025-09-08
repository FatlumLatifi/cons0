using var fs = File.Open("31.86476.csv", FileMode.Open, FileAccess.Read);

var bezirk = new Bezirk();
bezirk.ParseAdresses(fs);


using FileStream fs0 = File.Open("31.csv", FileMode.Create, FileAccess.Write);
using StreamWriter writer = new StreamWriter(fs0);
foreach (Zirk z in bezirk.Zirke)
{
    writer.WriteLine($"{z.Id},{z.GoogleMapsDirections}");
    Console.WriteLine($"{z.Id},{z.GoogleMapsDirections}");
}
