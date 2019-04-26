#----------斐波那契數列(遞迴操作)

Fibonacci_recursive   <- function(x)
{
  
  if  (x <= 2) return (1)
  
  else return (Fibonacci_recursive(x-2) + Fibonacci_recursive(x-1))
  
}

#----------斐波那契數列(非遞迴操作)

Fibonacci             <- function(n)
{
  
  f1 <- 1
  f2 <- 1
  
  if(n <= 2) return(f2)
  
  else
  {
    for(i in c(3 : n))
    {
      temp <- f2
      f2   <- f1 + f2
      f1   <- temp
    }
    return(f2)
  }
}

#----------驗證結果_斐波那契數列(遞迴、非遞迴操作)

cat("Fibonacci(9): ", Fibonacci(9), '\n')
cat("Fibonacci_recursive(9): ", Fibonacci_recursive(9), "\n\n")

cat("Fibonacci(58): ", Fibonacci(58), '\n')
cat("Fibonacci_recursive(42): ", Fibonacci_recursive(42))

