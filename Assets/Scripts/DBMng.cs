using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBMng
{
    private const string VOLUME = "volume";
    private const string ZUMBI_MORTOS = "zumbi_mortos";
    private const string TEMPO_JOGO = "tempo_jogo";

    public static void SalvarDados(int totalZumbisMortos, int totalTempoJogo){
        int zumbiMortos = PlayerPrefs.GetInt(ZUMBI_MORTOS,0);
        int tempoJogo = PlayerPrefs.GetInt(TEMPO_JOGO,0);
        if(zumbiMortos < totalZumbisMortos){
            PlayerPrefs.SetInt(ZUMBI_MORTOS,zumbiMortos);
        }
        if(tempoJogo < totalTempoJogo){
            PlayerPrefs.SetInt(TEMPO_JOGO,tempoJogo);
        }
    }

    public static void SalvarVolume(float volumeVFX, float volumeMusica){
        Volume volume = new Volume();
        volume.vfx = volumeVFX;
        volume.musica = volumeMusica;
        var json = JsonUtility.ToJson(volume);
        PlayerPrefs.SetString(VOLUME,json);
    }

    public static Volume ObterVolumes(){
        var json = PlayerPrefs.GetString(VOLUME);
        Volume volume = JsonUtility.FromJson<Volume>(json);
        if(volume == null){
            SalvarVolume(0.5f,0.2f);
            json = PlayerPrefs.GetString(VOLUME);
            volume = JsonUtility.FromJson<Volume>(json);
        }

        return volume;
    }
}
