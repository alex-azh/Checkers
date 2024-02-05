namespace CheckersGame.Evaluaters;

public class RandomPredictor : IPredictor
{
    public float[] Predict(bool[][] array) => Enumerable.Range(0, array.Length).Select(m => Random.Shared.NextSingle()).ToArray();

    
    public void Save(string modelFilePath)
    {
        throw new NotImplementedException();
    }
    public void Load(string modelFilePath)
    {
        throw new NotImplementedException();
    }
}
