using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;

namespace RiskSrc.Model.IO {
    public class JsonFile {

        private static readonly Dictionary<AbsolutePath, JsonFile> backup 
            = new Dictionary<AbsolutePath, JsonFile>();
        
        public static JsonFile GetJsonFile(AbsolutePath atPath) {
            if (! backup.ContainsKey(atPath))
                backup[atPath] = new JsonFile(atPath);
            return backup[atPath];
        }

        private readonly AbsolutePath path;
        private JSONNode jsonNode;
        private JsonFile(AbsolutePath path) {
            this.path = path;
            var pathStr = File.Exists(path.Runtime) ? path.Runtime : path.Default;
            string jsonStr;
            if (Application.platform == RuntimePlatform.Android) {
                var reader = new WWW(pathStr);
                while (!reader.isDone) { }
                jsonStr = reader.text;
            }
            else{
                jsonStr = File.ReadAllText(pathStr);
            }                
            jsonNode = JSON.Parse(jsonStr);
            
        }
        
        public JSONNode Read() {
            return jsonNode;
        }

        public void Write(JSONNode data) {
            jsonNode = data;
        }

        public void Flush() {
            var tmpPathStr = path.Runtime;
            var slash = new[]{'/','\\'};
            var slashIndex = tmpPathStr.LastIndexOfAny(slash);
            var directory = tmpPathStr.Remove(startIndex: slashIndex + 1);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            else if (!File.Exists(tmpPathStr)) 
                File.Delete(tmpPathStr);
            File.Create(tmpPathStr).Close();
            using (var writer = new StreamWriter(path.Runtime)) {
                writer.WriteLine(jsonNode.ToString());
                writer.Flush();
                writer.Close();
            }
        }
    }
    
    public static class JSONNodeExtension{
        public static Color? AsColor(this JSONNode data) {
            if (data["R"].IsNull || data["G"].IsNull || data["B"].IsNull) return null;
            return new Color(
                r: data["R"].AsInt / 255f, 
                g: data["G"].AsInt / 255f, 
                b: data["B"].AsInt / 255f
            );
        }
        
        public static Texture2D AsTexture2D(this JSONNode data) {
            return Resources.Load<Texture2D>(data.Value);
        }
    }
    
    
    public static class FilePath {
        private static readonly JSONNode path 
            = JsonFile.GetJsonFile(new AbsolutePath(@"/Path.json")).Read();
        public static readonly AbsolutePath RuleSettingAbsPath = new AbsolutePath(path["RuleSettingPath"]);
        public static readonly AbsolutePath DecorationSettingAbsPath = new AbsolutePath(path["DecorationSettingPath"]);
        public static readonly AbsolutePath ContinentsDataAbsPath = new AbsolutePath(path["ContinentsDataPath"]);
        public static readonly AbsolutePath AreasDataAbsPath = new AbsolutePath(path["CountriesDataPath"]);
        public static readonly AbsolutePath PlayersDataAbsPath = new AbsolutePath(path["PlayersDataPath"]);
    }

    public struct AbsolutePath {

        private const string DATA_TMP_RELATIVE_PATH = @"/Tmp/";
        private const string JSON_POSTFIX = @".json";


        private readonly string relativePath;

        public string Default => Application.streamingAssetsPath + relativePath;
        public string Runtime => Application.streamingAssetsPath + DATA_TMP_RELATIVE_PATH + relativePath;
        
        public AbsolutePath(string relativePath) {
            if (!relativePath.EndsWith(JSON_POSTFIX)) 
                throw new InvalidDataException("The Path should be js format");
            this.relativePath = relativePath;
        }
    }
   
}