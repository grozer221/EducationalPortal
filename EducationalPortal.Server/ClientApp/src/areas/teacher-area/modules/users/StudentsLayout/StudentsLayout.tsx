import React, {FC} from 'react';
import {Route, Routes} from 'react-router-dom';
import {Error} from '../../../../../components/Error/Error';
import {StudentsIndex} from '../pages/StudentsIndex/StudentsIndex';
import {StudentsCreate} from '../pages/StudentsCreate/StudentsCreate';

export const StudentsLayout: FC = () => {
    return (
        <Routes>
            <Route path={'/'} element={<StudentsIndex/>}/>
            {/*<Route path={':id'} element={<SubjectsView/>}/>*/}
            <Route path={'create'} element={<StudentsCreate/>}/>
            {/*<Route path={'update/:id'} element={<SubjectsUpdate/>}/>*/}
            <Route path={'*'} element={<Error/>}/>
        </Routes>
    );
};
