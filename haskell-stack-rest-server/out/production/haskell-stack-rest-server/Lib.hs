module Lib
    ( startGame
    ) where

array :: [Int]
array = [4, 3, 6, 2, 5, 1, 8, 7, 9]

isSorted :: [Int] -> Bool
isSorted [1, 2, 3, 4, 5, 6, 7, 8, 9] = True
isSorted _ = False

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

startGame :: IO ()
startGame = loop array
