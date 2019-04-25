
recursive.factorial   <- function(x)
{
  if     (x <= 2)
  {
    return (1)
  }
  else
  {
    return (recursive.factorial(x-2)+recursive.factorial(x-1))
  }
}

print(recursive.factorial(5))

Fibonacci <- function(n)
{
  f1 <- 1
  f2 <- 1
  if(n == 1|n == 2)
  {
    return(f2)
  }
  else
  {
    for(i in 3:n)
    {
      temp1 <- f2
      f2 <- f1 + f2
      f1 <- temp1
    }
    return(f2)
  }
}

print(Fibonacci(6))