import { faLeftLong } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";


const PrevArrow = ({ onClick }) => {
    return (
      <div className="prevArrow block -left-[30px] sm:-left-[50px] lg:-left-[100px] top-[50%] absolute z-20 cursor-pointer -translate-y-[50%]" onClick={onClick} >
        <FontAwesomeIcon icon={faLeftLong} color="var(--primary-color)" size="2x" />
      </div>
    );
  };

export default PrevArrow;