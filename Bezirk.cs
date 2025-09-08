

using System.Security.Cryptography.X509Certificates;

public class Bezirk
{
    public Bezirk(int id = 31, int PLZ = 86476)
    {
        this.PLZ = PLZ;
        this.Id = id;
        Zirke = [];
    }

    public int Id { get; set; }

    public int PLZ { get; set; }

    public List<Zirk> Zirke { get; set; }



    public void ParseAdresses(FileStream fileStream)
    {
        using var reader = new StreamReader(fileStream);

        string? line;
        Adresse address = new();
        address.PLZ = this.PLZ;
        int lastZirk = 1;
        Zirk z = new();
        while ((line = reader.ReadLine()) != null)
        {
            if (line.Length < 5) continue;
            ReadOnlySpan<string> strings = line.Split(',');
            int currentZab = int.Parse(strings[0]);
            address.zAb = currentZab;


            if (currentZab != lastZirk)
            {
                z.Id = lastZirk;
                this.Zirke.Add(z);
                lastZirk = currentZab;
                z = new Zirk();
            }

            z.Id = currentZab;
            
            address.Strasse = strings[1];

            if (strings[2].Contains('-'))
            {
                ReadOnlySpan<string> hausnummern = strings[2].Split('-');
                foreach (var hn in hausnummern)
                {
                    address.Hausnummer = hn;
                    z.Adressen.Add(address);
                }
            }
            else
            {
                address.Hausnummer = strings[2]; ;
                z.Adressen.Add(address);
            }
        }
        this.Zirke.Add(z);
    }



}


public struct Zirk
{

    public Zirk()
    {
        Adressen = [];
    }
    public int Id { get; set; }

    public List<Adresse> Adressen { get; set; }
}


public struct Adresse
{
    public int zAb { get; set; }
    public string Strasse { get; set; }

    public string Hausnummer { get; set; }

    public int PLZ { get; set; }

}