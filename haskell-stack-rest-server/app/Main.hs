{-# LANGUAGE OverloadedStrings #-}

module Main (main) where

import Lib (response)
import Web.Scotty

main :: IO ()
main = scotty 3000 $ do
  get "/:word" $ do
    beam <- param "word"
    html (response beam)
