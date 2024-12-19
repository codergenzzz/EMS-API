namespace EMS_API.Controllers
{
    public class Actor
    {
        static public Dictionary<string, Actor> Actors = new Dictionary<string, Actor>();
        static public Actor CreateActor(string role)
        {
            return (Actor)Activator.CreateInstance(Type.GetType($"EMS_API.Controllers.{role}"));
        }

        public object? LoginSuccess(string token)
        {
            Actors.Add(token, this);
            return this;
        }

        public Dtos.ProfileDto? Profile { get; set; }
        public object? ValueContext { get; set; }
    }

    public class Admin : Actor
    {
    }

    public class Customer : Actor
    {
    }
}
