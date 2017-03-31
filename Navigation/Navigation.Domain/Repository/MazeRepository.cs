using System.Collections.Generic;
using System.IO;
using System.Linq;
using Navigation.Domain.Game.Mazes;
using Navigation.Domain.Repository.Representations;
using Newtonsoft.Json;

namespace Navigation.Domain.Repository
{
    public class MazeRepository : IMazeRepository
    {
        private readonly DirectoryInfo _directory;
        private readonly List<string> _mazeNames;
        
        public MazeRepository(string path)
        {
            _directory = new DirectoryInfo(path);

            if(!_directory.Exists)
                _directory.Create();

            _mazeNames =
                _directory.GetFiles()
                    .Select(file => file.Name.Replace(file.Extension, "")).ToList();
        }

        public IReadOnlyList<string> MazeNames => _mazeNames;

        public bool HaveMaze(string name)
        {
            return _mazeNames.Any(z => z == name);
        }

        public void Save(IMaze maze, string name)
        {
            File.WriteAllText(PathTo(name), JsonConvert.SerializeObject(new MazeRepresentation(maze)));

            _mazeNames.Add(name);
            _mazeNames.Sort();
        }

        public IMaze Load(string name)
        {
            return (StandartMaze)JsonConvert.DeserializeObject<MazeRepresentation>(File.ReadAllText(PathTo(name))); ;
        }

        public void Delete(string name)
        {
            GetMazeFile(name).Delete();

            _mazeNames.Remove(name);
        }

        public void ChangeName(string nowName, string newName)
        {
            GetMazeFile(nowName).MoveTo(PathTo(newName));
            
            _mazeNames.Remove(nowName);
            _mazeNames.Add(newName);
            _mazeNames.Sort();
        }
        
        private string PathTo(string name)
        {
            return _directory.FullName + name + ".txt";
        }

        private FileInfo GetMazeFile(string name)
        {
            return _directory.GetFiles().Single(file => file.Name.Replace(file.Extension, "") == name);
        }
    }
}
