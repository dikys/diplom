using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Navigation.Domain.Repository.Representations;
using Newtonsoft.Json;

namespace Navigation.Domain.Repository
{
    public class MazeRepository
    {
        public string Path { get; }

        public MazeRepository(string path)
        {
            Path = path;
        }

        public void Save(Maze.Maze maze, string name)
        {
            File.WriteAllText(PathTo(name), JsonConvert.SerializeObject(new MazeRepresentation(maze)));
        }

        public Maze.Maze Load(string name)
        {
            return (Maze.Maze)JsonConvert.DeserializeObject<MazeRepresentation>(File.ReadAllText(PathTo(name)));
        }

        public IReadOnlyList<string> GetMazes()
        {
            return Directory.GetFiles(Path);
        }

        private string PathTo(string name)
        {
            return Path + name + ".txt";
        }
    }
}
