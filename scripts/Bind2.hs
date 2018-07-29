-- stack runghc --package strict

bind = (>>=)

main :: IO()
main =
    bind(putStrLn "Enter your name")
        (\_ -> bind getLine
            (\name -> putStrLn $ "Hello " ++ name ++ ". It's nice to meet you.")
        )