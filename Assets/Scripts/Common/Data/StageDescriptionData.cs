using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common.Data
{
    [CreateAssetMenu(fileName = "StageDescriptionData", menuName = "ScriptableObjects/StageDescriptionData")]
    public class StageDescriptionData : ScriptableObject
    {
        [SerializeField] private List<StageDescription> stageDescriptionList;
        public string GetStageNameJA(int index) => stageDescriptionList[index].StageNameJA;
        public string GetDescriptionJA(int index) => stageDescriptionList[index].DescriptionJA;
        public string GetStageNameEN(int index) => stageDescriptionList[index].StageNameEN;
        public string GetDescriptionEN(int index) => stageDescriptionList[index].DescriptionEN;
    }

    [Serializable]
    class StageDescription  //インスペクターでステージ名と説明文をまとめて編集するためにクラス化
    {
        [SerializeField] private string stageNameJA;
        [SerializeField, TextArea(3, 10)] private string descriptionJA;
        [SerializeField] private string stageNameEN;
        [SerializeField, TextArea(3, 10)] private string descriptionEN;

        public string StageNameJA => stageNameJA;
        public string DescriptionJA => descriptionJA;
        public string StageNameEN => stageNameEN;
        public string DescriptionEN => descriptionEN;
    }
}
