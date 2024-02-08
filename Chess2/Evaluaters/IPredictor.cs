namespace CheckersGame.Evaluaters;

public interface IPredictor
{
    float[] Predict(float[][] array);
    void Load(string modelFilePath);
    void Save(int epochNumber, string modelFilePath);
}
