module Lib
    ( startGame
    ) where

import System.Random (newStdGen)
import System.Random.Shuffle (shuffle')

isSorted :: [Int] -> Bool
isSorted [] = True
isSorted [_] = True
isSorted (x:y:xs) = x <= y && isSorted (y:xs)

reverseFirst :: Int -> [Int] -> [Int]
reverseFirst n xs = reverse (take n xs) ++ drop n xs

getUserInput :: IO Int
getUserInput = putStrLn "How many first elements to reverse: " >> getLine >>= \line -> return (read line)

gameLoop :: [Int] -> IO ()
gameLoop array = do
  print array
  numberOfElements <- getUserInput
  let reversed = reverseFirst numberOfElements array
  if isSorted reversed then putStrLn "You won!" else gameLoop reversed

shuffledArray :: [Int] -> IO [Int]
shuffledArray xs = do
  gen <- newStdGen
  return (shuffle' xs (length xs) gen)

startGame :: IO ()
startGame = do
  array <- shuffledArray [1..9]
  gameLoop array
