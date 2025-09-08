using var fs = File.Open("31.86476.csv", FileMode.Open, FileAccess.Read);

var bezirk = new Bezirk();
bezirk.ParseAdresses(fs);

foreach(Zirk z in bezirk.Zirke)
{
    Console.WriteLine($"Zirk {z.Id} has {z.Adressen.Count} addresses.");
}
