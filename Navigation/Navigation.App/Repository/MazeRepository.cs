using System.Collections.Generic;
using System.IO;
using System.Linq;
using Navigation.App.Repository.Representations;
using Navigation.Domain.Mazes;
using Newtonsoft.Json;

namespace Navigation.App.Repository
{
    public class MazeRepository
    {
        private readonly DirectoryInfo _directory;
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
            return (DefaultMaze)JsonConvert.DeserializeObject<MazeRepresentation>(File.ReadAllText(PathTo(name)));
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
