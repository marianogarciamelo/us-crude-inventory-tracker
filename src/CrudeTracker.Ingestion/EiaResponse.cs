namespace CrudeTracker.Ingestion;

record EiaResponse(EiaData Response); 
record EiaData(List<EiaRow> Data);
record EiaRow(string Period, decimal Value, string Series);