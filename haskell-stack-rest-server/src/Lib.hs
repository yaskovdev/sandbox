module Lib (response) where

import qualified Data.Text.Lazy as L

response :: String -> L.Text
response beam = L.pack (concat ["<h1>Scotty, ", beam, " me up!</h1>"])
