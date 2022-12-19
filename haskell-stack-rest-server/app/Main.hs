{-# LANGUAGE OverloadedStrings #-}

module Main (main) where

import qualified Data.Text.Lazy as L
import Lib (response)
import Web.Scotty

main :: IO ()
main = scotty 3000 $ do
  get "/:word" $ do
    beam <- param "word"
    html (L.pack (response beam))
  post "/moves/:move" $ do
    move <- param "move"
    html (L.pack (response move))
