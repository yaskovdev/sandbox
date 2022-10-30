module Lib
    ( startGame
    ) where

import System.Random
import System.Random.Shuffle (shuffle')

isSorted :: [Int] -> Bool
isSorted [] = True
isSorted [_] = True
isSorted (x:y:xs) = x <= y && isSorted (y:xs)

reverseFirst :: Int -> [Int] -> [Int]
reverseFirst n xs = reverse (take n xs) ++ drop n xs

getUserInput :: IO Int
getUserInput = print "How many elements to reverse: " >> getLine >>= \line -> return (read line)

loop :: [Int] -> IO ()
loop xs = do
  print xs
  input <- getUserInput
  let reversed = reverseFirst input xs
  if isSorted reversed then putStrLn "You won!" else loop reversed
  
shuffledArray :: [Int] -> IO [Int]
shuffledArray xs = do
  gen <- newStdGen
  return (shuffle' xs (length xs) gen)

startGame :: IO ()
startGame = do
  xs <- shuffledArray [1..9]
  loop xs
