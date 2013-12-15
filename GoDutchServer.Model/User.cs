using System;

namespace GoDutchServer.Model
{
    [Serializable]
    public class MyUser
    {
        public int ID { get; set; }
        public string UserName { get; set; }
    }
}