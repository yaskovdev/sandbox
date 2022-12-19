module Lib (response) where

response :: String -> String
response beam = concat ["<h1>Scotty, ", beam, " me up!</h1>"]
