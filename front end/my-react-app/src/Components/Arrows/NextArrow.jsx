import { faRightLong } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const NextArrow = ({ onClick }) => {
    return (
      <div className="nextArrow prevArrow block -right-[30px]  sm:-right-[50px] lg:-right-[100px] top-[50%] absolute z-20 cursor-pointer -translate-y-[50%]" onClick={onClick} >
        <FontAwesomeIcon icon={faRightLong} color="var(--primary-color)" size="2x" />
      </div>
    );
  };

export default NextArrow;