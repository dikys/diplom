using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Navigation.App.Repository.Representations;
using Navigation.Domain.Mazes;
using Newtonsoft.Json;

namespace Navigation.App.Repository
{
    public class MazeRepository : IMazeRepository
    {
        private readonly DirectoryInfo _directory;
        
        public MazeRepository(string path)
        {
            _directory = new DirectoryInfo(path);

            if(!_directory.Exists)
                _directory.Create();
        }

        public event Action<string, string> CommandExecuted;
        public event Action<string, string> CommandError;
        public event Action<string> AddedMaze;
        public event Action<string> RemovedMaze;
        
        public string Path => _directory.FullName;
        public IEnumerable<string> MazeNames
            => _directory.GetFiles().Select(file => file.Name.Replace(file.Extension, ""));
        public IEnumerable<StandartMaze> Mazes
            => MazeNames.Select(Load);
        
        public void Save(StandartMaze maze, string name)
        {
            name = name.Trim();

            if (name == "")
            {
                CommandError?.Invoke("Save", "Имя лабиринта не может быть пустым");
                return;
            }
            else if (MazeNames.Any(z => z == name))
            {
                CommandError?.Invoke("Save", "Лабиринт с таким именем уже есть");
                return;
            }

            File.WriteAllText(PathTo(name), JsonConvert.SerializeObject(new MazeRepresentation(maze)));

            AddedMaze?.Invoke(name);
            CommandExecuted?.Invoke("Save", "Сохранено успешно");
        }

        public StandartMaze Load(string name)
        {
            if (MazeNames.All(z => z != name))
            {
                CommandError?.Invoke("Load", "Такого лабиринта не существует");
            }

            var result = (StandartMaze)JsonConvert.DeserializeObject<MazeRepresentation>(File.ReadAllText(PathTo(name)));

            CommandExecuted?.Invoke("Load", "Загруженно успешно");

            return result;
        }

        public void Delete(string name)
        {
            try
            {
                _directory.GetFiles().Single(file => file.Name.Replace(file.Extension, "") == name).Delete();

                RemovedMaze?.Invoke(name);
                CommandExecuted?.Invoke("Delete", "Удаленно успешно");
            }
            catch (ArgumentNullException)
            {
                CommandError?.Invoke("Delete", "Лабиринта с таким именем не существует");
            }
        }
        
        private string PathTo(string name)
        {
            return Path + name + ".txt";
        }
    }
}
