namespace Model.Core.Contacts;

public class ContactInfo
{
    protected string _phone;
    protected string _website;

    public string Phone => _phone;
    public string Website => _website;

    public ContactInfo()
    {
        _phone = string.Empty;
        _website = string.Empty;
    }

    public ContactInfo(string phone, string website)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            throw new ArgumentException("Телефон не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(website))
        {
            throw new ArgumentException("Сайт не может быть пустым.");
        }

        _phone = phone;
        _website = website;
    }

    public void ChangePhone(string newPhone)
    {
        if (string.IsNullOrWhiteSpace(newPhone))
        {
            throw new ArgumentException("Телефон не может быть пустым.");
        }

        _phone = newPhone;
    }

    public void ChangeWebsite(string newWebsite)
    {
        if (string.IsNullOrWhiteSpace(newWebsite))
        {
            throw new ArgumentException("Сайт не может быть пустым.");
        }

        _website = newWebsite;
    }

    public bool IsEmpty()
    {
        return string.IsNullOrWhiteSpace(Phone) && string.IsNullOrWhiteSpace(Website);
    }

    public override string ToString()
    {
        if (IsEmpty())
        {
            return "Контакты не указаны";
        }

        return $"Телефон: {Phone} | Сайт: {Website}";
    }
}
