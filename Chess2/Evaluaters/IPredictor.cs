namespace CheckersGame.Evaluaters;

public interface IPredictor
{
    float[] Predict(bool[][] array);
}
