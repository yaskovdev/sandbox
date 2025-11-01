{-# LANGUAGE OverloadedStrings #-}
module Main (main) where

import Data.Aeson
import Lib
import qualified Data.Yaml             as Yaml
import qualified Data.ByteString.Char8 as S8
import Network.HTTP.Simple

data Auth = Auth String String

instance ToJSON Auth where
  toJSON (Auth apiKey secretApiKey) =
    object ["apikey" .= apiKey, "secretapikey" .= secretApiKey]

auth :: Auth
auth = Auth "my_api" "my_secret_api"

data Bundle = Bundle String String deriving (Show)

instance FromJSON Bundle where
  parseJSON value =
    case value of
      Object obj -> do
        privateKey <- obj .: "privatekey"
        certificateChain <- obj .: "certificatechain"
        return (Bundle privateKey certificateChain)
      _ -> fail "Expected an object for Bundle"

main :: IO ()
main = do
  let request = setRequestBodyJSON auth $ "POST http://httpbin.org/post"
  response <- httpJSON request
  putStrLn $ "The status code was: " ++ show (getResponseStatusCode response)
  print $ getResponseHeader "Content-Type" response
  let body = getResponseBody response :: Bundle
  print body
