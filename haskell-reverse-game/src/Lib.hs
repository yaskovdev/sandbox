module Lib (startGame) where

import System.Random (newStdGen)
import System.Random.Shuffle (shuffle')
import Text.Read (readMaybe)

isSorted :: [Int] -> Bool
isSorted [] = True
isSorted [_] = True
isSorted (x : y : xs) = x <= y && isSorted (y : xs)

reverseFirst :: Int -> [Int] -> [Int]
reverseFirst n array = reverse (take n array) ++ drop n array

zeroIfNothing :: Maybe Int -> Int
zeroIfNothing (Just a) = a
zeroIfNothing Nothing = 0

getUserInput :: IO Int
getUserInput = do
  putStrLn "How many first elements to reverse: "
  zeroIfNothing . readMaybe <$> getLine

gameLoop :: [Int] -> IO ()
gameLoop array = do
  print array
  numberOfElements <- getUserInput
  let reversedArray = reverseFirst numberOfElements array
  if isSorted reversedArray then putStrLn "You won!" else gameLoop reversedArray

shuffledArray :: [Int] -> IO [Int]
shuffledArray xs = shuffle' xs (length xs) <$> newStdGen

startGame :: IO ()
startGame = do
  array <- shuffledArray [1 .. 9]
  gameLoop array
