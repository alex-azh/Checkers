﻿namespace CheckersGame.Evaluaters;

public interface IPredictor
{
    float[] Predict(bool[][] array);
    void Load(string modelFilePath);
    void Save(int epochNumber, string modelFilePath);
}
