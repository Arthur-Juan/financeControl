using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Entities;

public class Department : BaseEntity
{
    public string? Name { get; private set; }
    public string? Slug { get; private set; }
    public List<Sector>? Sectors { get; private set; }
    public List<User>? Users { get; set; }
    public User Owner { get; set; }

    public Department(string name)
    {
        Name = name;
        Sectors ??= new List<Sector>
        {
            new Sector
            {
                Name = "All"
            }
        };

        if (Slug == string.Empty)
        {
            Slugify();
            
        }
    }


    public void Slugify()
    {
        
            // Remove special characters, replace spaces with hyphens, and convert to lowercase
            var slug = Name?.Trim()
                .ToLower()
                .Replace(" ", "-")
                .Replace("ä", "ae")
                .Replace("ö", "oe")
                .Replace("ü", "ue");

            // Remove diacritics (accents)
            slug = RemoveDiacritics(slug);

            // Remove any other special characters that are not letters, numbers, or hyphens
            slug = Regex.Replace(slug, @"[^a-z0-9-]", "");

            // Replace multiple hyphens with a single hyphen
            slug = Regex.Replace(slug, @"-+", "-");
            Slug = slug;
    }
    
    
    private string RemoveDiacritics(string? text)
    {
        var normalized = text.Normalize(NormalizationForm.FormKD);
        var removal = Encoding.GetEncoding(Encoding.ASCII.CodePage,
            new EncoderReplacementFallback(string.Empty),
            new DecoderReplacementFallback(string.Empty));

        var bytes = removal.GetBytes(normalized);
        return Encoding.ASCII.GetString(bytes);
    }

    public bool SetOwner(User? user)
    {
        if (user == null)
        {
            return false;
        }

        Owner = user;
        return true;
    }
}