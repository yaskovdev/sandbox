module Lib(someFunc, fact) where

someFunc :: IO ()
someFunc = putStrLn "someFunc"

fact :: Int -> Int
fact 0 = 1
fact n = n * fact (n - 1)
