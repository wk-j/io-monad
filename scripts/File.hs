import System.IO
import Data.Char (toUpper)

mainloop :: Handle -> Handle -> IO()
mainloop input output =
    do
        inputEof <- hIsEOF input
        if inputEof then
           return ()
        else
            do
                inputStr <- hGetLine input
                hPutStrLn output (map toUpper inputStr)
                mainloop input output

main :: IO()
main = do
    input <- openFile "resource/Input.txt" ReadMode
    output <- openFile "resource/Output.txt" WriteMode
    mainloop input output
    hClose input
    hClose output
