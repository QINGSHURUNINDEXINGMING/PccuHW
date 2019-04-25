
recursive.factorial   <- function(x)
{
  testSeq             <- c(0)


  if     (x <= 2) return (append(testSeq, recursive.factorial(x-1)))
  else            return (append(testSeq, (recursive.factorial(x-1)+testSeq[x-1])))

}

print(recursive.factorial(3))