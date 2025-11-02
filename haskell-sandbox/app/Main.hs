{-# LANGUAGE OverloadedStrings #-}

module Main (main) where

import Data.Aeson
import Data.ByteString as B
import Data.ByteString.UTF8 (fromString)
import Network.HTTP.Simple
import System.Environment (getArgs)
import Text.Printf (printf)

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

parseArgs :: [String] -> (String, Auth)
parseArgs [domain, apiKey, secretApiKey] = (domain, Auth apiKey secretApiKey)
parseArgs _ = error "Expected three arguments: domain, API key and secret API key"

certChain :: Bundle -> String
certChain (Bundle _ certificateChain) = certificateChain

main :: IO ()
main = do
  args <- getArgs
  let (domain, auth) = parseArgs args
  let url = printf "https://api.porkbun.com/api/json/v3/ssl/retrieve/%s" domain
  let request = setRequestBodyJSON auth (parseRequest_ $ "POST " ++ url)
  response <- httpJSON request
  putStrLn $ "The status code was: " ++ show (getResponseStatusCode response)
  let bundle = getResponseBody response :: Bundle
  let certificateChain = certChain bundle
  let certificateChainBytes = fromString certificateChain
  B.writeFile "domain.cert.pem" certificateChainBytes
  print certificateChainBytes
