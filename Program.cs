class Program{
    static void Main(string[] args){//args[0] es la ruta de ..\Cache\temp.tmp
        string path_temp=args[0];
        string path_log=AppContext.BaseDirectory+"log_plugin.txt";//Ubicacion del log_plugin.txt
        //Para enterarme si fue ejecutado
        //////////////////////////////////
        File.WriteAllText(path_log,"Plugin ejecutado.\n");//creo el log
        /////////////////////////////////
        string[] Data_OpenUtau = File.ReadAllLines(path_temp);
        var dictionary_lyric =new Dictionary<string, string>(){
            {"ca","ka"},{"ce","se"},{"ci","si"},{"co","ko"},{"cu","ku"},
            {"lla","ya"},{"lle","ye"},{"lli","yi"},{"llo","yo"},{"llu","yu"},// "lli" suena a "i"
            {"ha","a"},{"he","e"},{"hi","i"},{"ho","o"},{"hu","u"},
            {"ja","ha"},{"je","he"},{"ji","hi"},{"jo","ho"},{"ju","hu"},
        //    {"la","o r,a"},{"le","o r,e"},{"li","o r,i"},{"lo","o r,o"},{"lu","o r,u"},
            {"ña","nya"},{"ñe","nye"},{"ñi","nyi"},{"ño","nyo"},{"ñu","nyu"},
        //    {"qu","cu"},{"que","ke"},{"qui","ki"},{"",""},{"",""},
        //    {"xa",""},{"xe",""},{"xi",""},{"xo",""},{"xu",""},
        };
        for (int i=0; i < Data_OpenUtau.Length; i++){
            if(Data_OpenUtau[i].StartsWith("Lyric=")){
                string data=Data_OpenUtau[i].Substring(6);
                if (dictionary_lyric.ContainsKey(data)){
                    Data_OpenUtau[i]="Lyric="+dictionary_lyric[data];
                }
            }
        }

        //Guardo los cambios
        File.WriteAllLines(path_temp,Data_OpenUtau);
        File.AppendAllText(path_log,"Finaliza Plugin\n");
    }
}
