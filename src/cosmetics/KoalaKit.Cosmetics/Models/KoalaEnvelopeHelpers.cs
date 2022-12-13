using KoalaKit.Cosmetics;

public static class KoalaEnvelopeHelpers
{
    public static KoalaEnvelope<T> BuildEnvelopeResult<T>(T data)
    {
        var envelope = new KoalaEnvelope<T>(data);
        return envelope;
    }

    public static KoalaEnvelope BuildEnvelopeResult(params KoalaError[] errors)
    {
        var envelope = new KoalaEnvelope(errors);
        return envelope;
    }
}