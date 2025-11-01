{-# LANGUAGE OverloadedStrings #-}

module Main (main) where

import Data.Aeson
import Network.HTTP.Simple
import System.Environment (getArgs)

data Auth = Auth String String

instance ToJSON Auth where
  toJSON (Auth apiKey secretApiKey) =
    object ["apikey" .= apiKey, "secretapikey" .= secretApiKey]

data Bundle = Bundle String String deriving (Show)

instance FromJSON Bundle where
  parseJSON value =
    case value of
      Object obj -> do
        privateKey <- obj .: "privatekey"
        certificateChain <- obj .: "certificatechain"
        return (Bundle privateKey certificateChain)
      _ -> fail "Expected an object for Bundle"

authFromArgs :: [String] -> Auth
authFromArgs [apiKey, secretApiKey] = Auth apiKey secretApiKey
authFromArgs _ = error "Expected two arguments: apiKey and secretApiKey"

main :: IO ()
main = do
  args <- getArgs
  let auth = authFromArgs args
  let request = setRequestBodyJSON auth "POST https://api.porkbun.com/api/json/v3/ssl/retrieve/yaskov.dev"
  response <- httpJSON request
  putStrLn $ "The status code was: " ++ show (getResponseStatusCode response)
  let body = getResponseBody response :: Bundle
  print body
