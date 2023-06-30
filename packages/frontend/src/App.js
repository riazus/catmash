import Navbar from "./Navbar"
import Leaderboard from "./pages/Leaderboard/Leaderboard"
import Arena from "./pages/Arena/Arena"
import { Route, Routes } from "react-router-dom"
import ErrorNotFound from "./pages/Error/ErrorNotFound"

function App() {
  return (
    <>
      <Navbar />
      <div className="container">
        <Routes>
          <Route path="/" element={<Arena />} />
          <Route path="/scores" element={<Leaderboard />} />
          <Route path="/*" element={<ErrorNotFound />} />
        </Routes>
      </div>
    </>
  )
}

export default App
