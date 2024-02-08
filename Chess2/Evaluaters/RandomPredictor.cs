namespace CheckersGame.Evaluaters;

public class RandomPredictor : IPredictor
{
    public float[] Predict(float[][] array) => Enumerable.Range(0, array.Length).Select(m => Random.Shared.NextSingle()).ToArray();
  
    public void Load(string modelFilePath)
    {
        throw new NotImplementedException();
    }

    public void Save(int epochNumber, string modelFilePath)
    {
        throw new NotImplementedException();
    }
}
