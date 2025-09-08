

using System.Runtime.InteropServices;
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
            
            address.Strasse = strings[1].Trim();

            if (strings[2].Contains('-'))
            {
                ReadOnlySpan<string> hausnummern = strings[2].Split('-');
                foreach (var hn in hausnummern)
                {
                    address.Hausnummer = hn.Trim();
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

    public string GoogleMapsDirections
    {
        get
        {
            string url = "https://www.google.com/maps/dir/?api=1&origin=Current+Location";

            url += $"&destination={Adressen[Adressen.Count - 1].GoogleMapsUrl}";
            url += $"&travelmode=driving&dir_action=navigate";

            url += "&waypoints=";
            if (Adressen.Count < 3)
            {
                url += $"{Adressen[0].GoogleMapsUrl}";
                return url.Replace(" ","");
            }
            for (int i = 0; i < Adressen.Count - 1; i++)
            {
                url += $"%7C{Adressen[i].GoogleMapsUrl}";
            }
            return url.Replace(" ","");
        }
    }
}


public struct Adresse
{
    public int zAb { get; set; }
    public string Strasse { get; set; }

    public string Hausnummer { get; set; }

    public int PLZ { get; set; }

    public string GoogleMapsUrl {get { return $"{Strasse.Replace(" ","+")}+{Hausnummer}+{PLZ}"; } }

}