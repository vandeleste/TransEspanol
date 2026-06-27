class Program{
    static void Main(string[] args){//args[0] es la ruta de ..\Cache\temp.tmp
        string path_temp=args[0];
        string path_log=AppContext.BaseDirectory+"log_plugin.txt";//Ubicacion del log_plugin.txt
        //Creo log
        File.WriteAllText(path_log,"Plugin ejecutado.\n");
        string[] Data_OpenUtau = File.ReadAllLines(path_temp);
        //muestro los datos del temp
        for (int i=0;i<Data_OpenUtau.Length ;i++){
            File.AppendAllText(path_log,Data_OpenUtau[i]+"\n");
        }

        var dictionary_lyric =new Dictionary<string, string>(){
            {"ca","ka"},{"ce","se"},{"ci","si"},{"co","ko"},{"cu","ku"},
            {"lla","ya"},{"lle","ye"},{"lli","yi"},{"llo","yo"},{"llu","yu"},// "lli" suena a "i"
            {"ha","a"},{"he","e"},{"hi","i"},{"ho","o"},{"hu","u"},
            {"ja","ha"},{"je","he"},{"ji","hi"},{"jo","ho"},{"ju","hu"},
        //    {"la","o r,a"},{"le","o r,e"},{"li","o r,i"},{"lo","o r,o"},{"lu","o r,u"},
        //    {"ña","nya"},{"ñe","nye"},{"ñi","nyi"},{"ño","nyo"},{"ñu","nyu"}, //NO Realiza este cambio ya que temp guarda la ñ como n.
        //    {"qu","cu"},{"que","ke"},{"qui","ki"},{"",""},{"",""},
        //    {"xa",""},{"xe",""},{"xi",""},{"xo",""},{"xu",""},
        };
// Parte una nota en dos
        //Busca la nota
        int j=0;
        while(j<Data_OpenUtau.Length && !Data_OpenUtau[j].StartsWith("[#0000]")){
            j++;
        }
        if(j>=Data_OpenUtau.Length){
            File.AppendAllText(path_log,"NO encontro nota\n");
        }
        //Cambia la longitud de la nota
        int length_note=int.Parse(Data_OpenUtau[j+1].Substring(7));//Length=480
        Data_OpenUtau[j+1]="Length="+length_note/2;
        //Ubicacion de la proxima data.Ej [#0xxx]
        int k=j+1;
        while(k<Data_OpenUtau.Length && !Data_OpenUtau[k].StartsWith("[#0")){
            k++;
        }
        //Guarda en un array aux la data
        string[] Data_aux_1=new string[k];//Crea un array que va a tener los primeros k elementos.(hasta la nota selccionada)
        Array.Copy(Data_OpenUtau, 0, Data_aux_1, 0, k);

        string[] Data_aux_2=new string[Data_OpenUtau.Length-k];//Va a quedarse con el resto del array
        Array.Copy(Data_OpenUtau, k, Data_aux_2, 0, Data_OpenUtau.Length-k);
        //Crea la nueva nota copiando la anterior pero cambiando la longitud
        string[] Data_note=new string[k-j];
        Array.Copy(Data_OpenUtau,j,Data_note,0,k-j);
        Data_note[0]="[#INSERT]";
        Data_note[1]="Length="+(length_note/2  +  length_note%2).ToString();
        //Guarda los cambios
        Data_OpenUtau =new string[Data_aux_1.Length+Data_aux_2.Length+Data_note.Length];
        Array.Copy(Data_aux_1,0,Data_OpenUtau,0,Data_aux_1.Length);
        Array.Copy(Data_note,0,Data_OpenUtau,k,Data_note.Length);
        Array.Copy(Data_aux_2,0,Data_OpenUtau,k+Data_note.Length,Data_aux_2.Length);
        //Guardo los cambios
        File.WriteAllLines(path_temp,Data_OpenUtau);


//muestro los datos del temp
        for (int i=0;i<Data_OpenUtau.Length ;i++){
            File.AppendAllText(path_log,Data_OpenUtau[i]+"\n");
        }

//
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
/*
void PartirEnDos(int:x,int:y)// proporcion x:y cada "x" elementos hay "y" elementos
*/    
    
}
