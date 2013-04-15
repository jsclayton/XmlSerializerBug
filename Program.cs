using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlSerializerBug
{
    [Serializable]
    [XmlRoot("Foos")]
    public class FooResponse
    {
        [XmlArray("Foos")]
        [XmlArrayItem("Foo", typeof(Foo))]
        public List<Foo> Foos { get; set; }
 
        public FooResponse()
        {
            Foos = new List<Foo>();
        }
    }

    [XmlRoot("Foo")]
    public class Foo
    {
        public string FooId { get; set; }
        public string FooType { get; set; }
        public string FooUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Permalink { get; set; }
        public string UserId { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var response = GenerateData();
            var serializedData = SerializeData(response);

            Console.WriteLine(serializedData);
        }

        private static FooResponse GenerateData()
        {
            FooResponse response = new FooResponse();
            for (int i = 0; i < 20; i++)
            {
                response.Foos.Add(new Foo
                    {
                        FooId = i.ToString(),
                        FooType = Guid.NewGuid().ToString(),
                        FooUrl = Guid.NewGuid().ToString(),
                        Title = Guid.NewGuid().ToString(),
                        Description = Guid.NewGuid().ToString(),
                        FullText = Guid.NewGuid().ToString(),
                        TimeStamp = DateTime.Now,
                        Permalink = Guid.NewGuid().ToString(),
                        UserId = Guid.NewGuid().ToString()
                    });
            }

            return response;
        }

        private static string SerializeData(FooResponse response)
        {
            var textwriter = new StringWriter();
            var serializer = new XmlSerializer(typeof (FooResponse));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add(String.Empty, String.Empty);
            serializer.Serialize(textwriter, response, namespaces);
            return textwriter.ToString();
        }
    }
}
