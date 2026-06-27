class Program{
    static void Main(string[] args){//args[0] es la ruta de ..\Cache\temp.tmp
        string path_temp=args[0];
        string path_log=AppContext.BaseDirectory+"log_plugin.txt";//Ubicacion del log_plugin.txt
        string[] Data_OpenUtau = File.ReadAllLines(path_temp);
        var diccionary_CCV =new Dictionary<string,string>(){
            {"bra","b,ra"},{"bre","b,re"},{"bri","b,ri"},{"bro","b,ro"},{"bru","b,ru"},
            {"bla","b,la"},{"ble","b,le"},{"bli","b,li"},{"blo","b,lo"},{"blu","b,lu"},
            {"pra","p,ra"},{"pre","p,re"},{"pri","p,ri"},{"pro","p,ro"},{"pru","p,ru"},
            {"pla","p,la"},{"ple","p,le"},{"pli","p,li"},{"plo","p,lo"},{"plu","p,lu"},
            {"tra","t,ra"},{"tre","t,re"},{"tri","t,ri"},{"tro","t,ro"},{"tru","t,ru"},
            {"cla","c,la"},{"cle","c,le"},{"cli","c,li"},{"clo","c,lo"},{"clu","c,lu"},
            {"cra","c,ra"},{"cre","c,re"},{"cri","c,ri"},{"cro","c,ro"},{"cru","c,ru"},
            {"gra","g,ra"},{"gre","g,re"},{"gri","g,ri"},{"gro","g,ro"},{"gru","g,ru"},
            {"fra","f,ra"},{"fre","f,re"},{"fri","f,ri"},{"fro","f,ro"},{"fru","f,ru"},
            {"rra","r,ra"},{"rre","r,re"},{"rri","r,ri"},{"rro","r,ro"},{"rru","r,ru"},
        };
        var diccionary_diptongo =new Dictionary<string,string>(){
            {"cia","si,a"},{"cie","si,e"},{"cio","si,o"},
            {"gia","gi,a"},{"gie","gi,e"},{"gio","gi,o"},
            {"bia","bi,a"},{"bie","bi,e"},{"bio","bi,o"},
            {"pia","pi,a"},{"pie","pi,e"},{"pio","pi,o"},
            {"ria","ri,a"},{"rie","ri,e"},{"rio","ri,o"},
            {"dia","di,a"},{"die","di,e"},{"dio","di,o"},
            {"gua","gu,a"},{"gue","gu,e"},{"gui","gu,i"},
            {"cua","ku,a"},{"cue","ku,e"},{"cui","ku,i"},
        };
        var diccionary_C=new Dictionary<string,string>(){
            {"b","ba"},{"p","pa"},{"t","te"},{"c","ka"},{"g","ge"},{"f","fe"},{"r","ra"},
        };
        var dictionary_CV =new Dictionary<string, string>(){
        //Cambian CV
            {"ca","ka"},{"ce","se"},{"ci","si"},{"co","ko"},{"cu","ku"},
            {"lla","ya"},{"lle","ye"},{"lli","yi"},{"llo","yo"},{"llu","yu"},// "lli" suena a "i"
            {"ha","a"},{"he","e"},{"hi","i"},{"ho","o"},{"hu","u"},
            {"ja","ha"},{"je","he"},{"ji","hi"},{"jo","ho"},{"ju","hu"},
            {"la","o r,a"},{"le","o r,e"},{"li","o r,i"},{"lo","o r,o"},{"lu","o r,u"},
        //    {"ña","nya"},{"ñe","nye"},{"ñi","nyi"},{"ño","nyo"},{"ñu","nyu"}, //NO Realiza este cambio ya que temp guarda la ñ como n.
            {"qu","ku"},{"que","ke"},{"qui","ki"},
        //    {"xa",""},{"xe",""},{"xi",""},{"xo",""},{"xu",""},
        //    {"gue",""},{"gui",""},
        //    {"wa",""},{"we",""},{"wi",""},{"wo",""},{"wu",""},
        /*Se quedan igual
ma me mi mo mu|pa pe pi po pu|sa se si so su|ta te ti to tu|na ne ni no nu|ga ge gi go gu|ba be bi bo bu|da de di do du|fa fe fi fo fu|ra re ri ro ru|va ve vi vo vu|cha che chi cho chu|ka ke ki ko ku|va ve vi vo vu|ya ye yi yo yu|za ze zi zo zu
        */
        };
        //Creo log
        File.WriteAllText(path_log,"Plugin ejecutado.\n");

        //muestro los datos del temp
        for (int i=0;i<Data_OpenUtau.Length ;i++){
            File.AppendAllText(path_log,Data_OpenUtau[i]+"\n");
        }

        
// Parte una nota en dos
        //Busca la nota
        int j=0;
        while(j<Data_OpenUtau.Length && !Data_OpenUtau[j].StartsWith("[#0000]")){
            j++;
        }
        if(j>=Data_OpenUtau.Length){
            File.AppendAllText(path_log,"NO encontro nota\n");
        }
        else{
            Data_OpenUtau=PartirEnDos(args,j,"a","o",5,1);
        }

/*
    recorre nota por nota//SIN TERMINAR
        lee Lyric=
        if(si coincide con el diccionario_CV o diccionary_C){
                cambio
        }else{
            if(ultima letra es r s n l){se parteEndos(letra,3,1)}
            if(si coincide con el diccionario CCV o diccionario diptongo){
            cambio por lo del diccionario
            parto nota}
        }

    int ind=0;//ind=[#0...]    
    while(ind<Data_OpenUtau.Length && !Data_OpenUtau[j].StartsWith("[#0")){
        string data=Data_OpenUtau[ind+1].Substring(6)//obtiene lo siguente de Lyric=
        if(dictionary_CV.ContainsKey(data)||dictionary_C.ContainsKey(data)){//NO Tengo que partir
            string data=Data_OpenUtau[ind+1].Substring(6);
            if (dictionary_CV.ContainsKey(data)){
                Data_OpenUtau[ind+1]="Lyric="+dictionary_CV[data];
            };
            if (dictionary_C.ContainsKey(data)){
                Data_OpenUtau[ind+1]="Lyric="+dictionary_CV[data];
            }
        }else{
            if(Data_OpenUtau[ind+2].Contains("l")||Data_OpenUtau[ind+2].Contains("r")||Data_OpenUtau[ind+2].Contains("n")||Data_OpenUtau[ind+2].Contains("s")){
                if(Data_OpenUtau[ind+2].Contains("l")){Data_OpenUtau=PartirEnDos(args,ind,data,"l",3,1);}
                if(Data_OpenUtau[ind+2].Contains("r")){Data_OpenUtau=PartirEnDos(args,ind,data,"r",3,1);}
                if(Data_OpenUtau[ind+2].Contains("s")){Data_OpenUtau=PartirEnDos(args,ind,data,"s",3,1);}
                if(Data_OpenUtau[ind+2].Contains("n")){Data_OpenUtau=PartirEnDos(args,ind,data,"n",3,1);}
            }
            if(dictionary_CCV.ContainsKey(data)){
                Data_OpenUtau[ind+2]="Lyric="+dictionary_CCV[data]
                

                Data_OpenUtau=PartirEnDos(args,ind,"a","o",1,1);
            }
            if(dictionary_diptongo.ContainsKey(data)){
                Data_OpenUtau=PartirEnDos(args,ind,"a","o",1,1);
            }
        }
        ind++;
        }

*/




        //Guardo los cambios
        File.WriteAllLines(path_temp,Data_OpenUtau);
        File.AppendAllText(path_log,"Finaliza Plugin\n");
    }

    static string[] PartirEnDos(string[]args,int j,string Lyric_1,string Lyric_2,int x,int y){//PartirEnDos(dir temp,indice de la nota,Lyric_1,Lyric_2,x,y) proporcion x:y cada "x" elementos hay "y" elementos, x=primera nota y=segunda nota
        string path_temp=args[0];
        string path_log=AppContext.BaseDirectory+"log_plugin.txt";//Ubicacion del log_plugin.txt
        string[] Data_OpenUtau = File.ReadAllLines(path_temp);
        //
        Data_OpenUtau[j+2]="Lyric="+Lyric_1;
        //Cambia la longitud de la nota
        int length_note=int.Parse(Data_OpenUtau[j+1].Substring(7));//Length=480
        Data_OpenUtau[j+1]="Length="+x*length_note/(x+y);
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
        Data_note[1]="Length="+(length_note -x*length_note/(x+y)).ToString();
        Data_note[2]="Lyric="+Lyric_2;
        //Guarda los cambios
        Data_OpenUtau =new string[Data_aux_1.Length+Data_aux_2.Length+Data_note.Length];
        Array.Copy(Data_aux_1,0,Data_OpenUtau,0,Data_aux_1.Length);
        Array.Copy(Data_note,0,Data_OpenUtau,k,Data_note.Length);
        Array.Copy(Data_aux_2,0,Data_OpenUtau,k+Data_note.Length,Data_aux_2.Length);
        
        return Data_OpenUtau;
    }
}

/*
        for (int i=0; i < Data_OpenUtau.Length; i++){
            if(Data_OpenUtau[i].StartsWith("Lyric=")){
                string data=Data_OpenUtau[i].Substring(6);
                if (dictionary_CV.ContainsKey(data)){
                    Data_OpenUtau[i]="Lyric="+dictionary_CV[data];
                }
            }
        }
*/