
import { Hearts } from 'react-loader-spinner';

const Loading = () => {
    return (
        <div className='w-100 h-100 flex justify-center items-center py-96  z-40'>
            <Hearts
  height="80"
  width="80"
  color="#CB997E"
  ariaLabel="hearts-loading"
  wrapperStyle={{}}
  wrapperClass=""
  visible={true}
  />
        </div>
    );
}

export default Loading;
