
FDegree_To_CDegree   <- function(x)
{

  CDegree            <- round(((x-32)*5/9), 2)
  CDegree_Final      <- paste(CDegree, sep="", "F")
  
  print(FDegree_Final)
  
}

Fahrenheit.Degree    <- seq(from=98, to=100.5, by=0.5)
FDegree_To_CDegree(Fahrenheit.Degree)