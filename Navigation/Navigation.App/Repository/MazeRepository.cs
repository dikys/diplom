using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Navigation.Domain.Repository.Representations;
using Newtonsoft.Json;
using Navigation.Domain.Maze;

namespace Navigation.Domain.Repository
{
    public class MazeRepository
    {
        private DirectoryInfo _directory;
        //private List<FileInfo> _mazeFiles; 

        public string Path => _directory.FullName;
        
        public MazeRepository(string path)
        {
            _directory = new DirectoryInfo(path);

            if(!_directory.Exists)
                _directory.Create();
        }

        public void Save(IMaze maze, string name)
        {
            File.WriteAllText(PathTo(name), JsonConvert.SerializeObject(new MazeRepresentation(maze)));
        }

        public IMaze Load(string name)
        {
            return (Maze.Maze)JsonConvert.DeserializeObject<MazeRepresentation>(File.ReadAllText(PathTo(name)));
        }

        public void Delete(string name)
        {
            _directory.GetFiles().Single(file => file.Name.Replace(file.Extension, "") == name).Delete();
        }

        public IReadOnlyList<string> GetMazeNames()
        {
            return _directory.GetFiles().Select(file => file.Name.Replace(file.Extension, "")).ToList();
        }

        private string PathTo(string name)
        {
            return Path + name + ".txt";
        }
    }
}
