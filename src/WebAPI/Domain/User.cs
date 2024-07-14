
public class User{
    public Guid Id{get; private set;} = Guid.NewGuid();
    public string Name{get; private set;}
    public string Email{get; private set;}
    public DateTime RegistrationTime{get; private set;}

    public User(string name, string email){
        Name = name;
        Email = email;
        RegistrationTime = DateTime.Now;
    }

    public User(Guid id, string name, string email){
        Id = id;
        Name = name;
        Email = email;
        RegistrationTime = DateTime.Now;
    }

    public void SetRegistrationTime(DateTime registrationTime){
        RegistrationTime = registrationTime;
    }
}